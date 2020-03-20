﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace OpenTKMapMaker.Utility
{
    /// <summary>
    /// Handles the file system cleanly.
    /// </summary>
    public class FileHandler
    {
        public List<PakFile> Paks = new List<PakFile>();

        public List<PakkedFile> Files = new List<PakkedFile>();

        /// <summary>
        /// The default text encoding.
        /// </summary>
        public static Encoding encoding = new UTF8Encoding(false);

        /// <summary>
        /// The base directory in which all data is stored.
        /// </summary>
        public static string BaseDirectory = Environment.CurrentDirectory.Replace("\\", "/") + "/data/";

        public void Init()
        {
            string[] allfiles = Directory.GetFiles(BaseDirectory, "*.*", SearchOption.AllDirectories);
            foreach (string tfile in allfiles)
            {
                string file = tfile.Replace('\\', '/');
                if (file.Length == 0 || file[file.Length - 1] == '/')
                {
                    continue;
                }
                if (file.EndsWith(".pak"))
                {
                    Paks.Add(new PakFile(file.Replace(BaseDirectory, "")));
                }
                else
                {
                    Files.Add(new PakkedFile(file.Replace(BaseDirectory, "")));
                }
            }
            int id = 0;
            foreach (PakFile pak in Paks)
            {
                List<ZipStorer.ZipFileEntry> zents = pak.Storer.ReadCentralDir();
                SysConsole.Output(OutputType.INIT, id + ") Pak " + pak.Name + " has " + zents.Count + " files.");
                pak.FileListIndex = Files.Count;
                foreach (ZipStorer.ZipFileEntry zent in zents)
                {
                    string name = zent.FilenameInZip.Replace('\\', '/').Replace("..", ".").Replace(":", "").ToLower();
                    if (name.Length == 0 || name[name.Length - 1] == '/')
                    {
                        continue;
                    }
                    SysConsole.Output(OutputType.INIT, "--> " + name);
                    Files.Add(new PakkedFile(name, id, zent));
                }
                id++;
            }
        }

        /// <summary>
        /// Cleans a file name for direct system calls.
        /// </summary>
        /// <param name="input">The original file name</param>
        /// <returns>The cleaned file name</returns>
        public static string CleanFileName(string input)
        {
            StringBuilder output = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                // Remove double slashes, or "./"
                if ((input[i] == '/' || input[i] == '\\') && output.Length > 0 && (output[output.Length - 1] == '/' || output[output.Length - 1] == '.'))
                {
                    continue;
                }
                // Fix backslashes to forward slashes for cross-platform folders
                if (input[i] == '\\')
                {
                    output.Append('/');
                    continue;
                }
                // Remove ".." (up-a-level) folders, or "/."
                if (input[i] == '.' && output.Length > 0 && (output[output.Length - 1] == '.' || output[output.Length - 1] == '/'))
                {
                    continue;
                }
                // Clean spaces to underscores
                if (input[i] == (char)0x00A0 || input[i] == ' ')
                {
                    output.Append('_');
                    continue;
                }
                // Remove non-ASCII symbols, ASCII control codes, and Windows control symbols
                if (input[i] < 32 || input[i] > 126 || input[i] == '?' ||
                    input[i] == ':' || input[i] == '*' || input[i] == '|' ||
                    input[i] == '"' || input[i] == '<' || input[i] == '>')
                {
                    output.Append('_');
                    continue;
                }
                // Lower-case letters only
                if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    output.Append((char)(input[i] - ('A' - 'a')));
                    continue;
                }
                // All others normal
                output.Append(input[i]);
            }
            // Limit length
            if (output.Length > 100)
            {
                // Also, trim leading/trailing spaces.
                return output.ToString().Substring(0, 100).Trim();
            }
            // Also, trim leading/trailing spaces.
            return output.ToString().Trim();
        }

        public int FileIndex(string filename)
        {
            string cleaned = CleanFileName(filename);
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].Name == cleaned)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns whether a file exists.
        /// </summary>
        /// <param name="filename">The name of the file to look for</param>
        /// <returns>Whether the file exists</returns>
        public bool Exists(string filename)
        {
            return FileIndex(filename) != -1;
        }

        /// <summary>
        /// Returns all the byte data in a file.
        /// </summary>
        /// <param name="filename">The name of the file to read</param>
        /// <returns>The file's data, as a byte array</returns>
        public byte[] ReadBytes(string filename)
        {
            int ind = FileIndex(filename);
            if (ind == -1)
            {
                throw new UnknownFileException(CleanFileName(filename));
            }
            PakkedFile file = Files[ind];
            if (file.IsPakked)
            {
                MemoryStream ms = new MemoryStream();
                Paks[file.PakIndex].Storer.ExtractFile(file.Entry, ms);
                byte[] toret = ms.ToArray();
                ms.Close();
                return toret;
            }
            else
            {
                return File.ReadAllBytes(BaseDirectory + file.Name);
            }
        }

        /// <summary>
        /// Returns a stream of the byte data in a file.
        /// </summary>
        /// <param name="filename">The name of the file to read</param>
        /// <returns>The file's data, as a stream</returns>
        public DataStream ReadToStream(string filename)
        {
            return new DataStream(ReadBytes(filename));
        }

        /// <summary>
        /// Returns all the text data in a file.
        /// </summary>
        /// <param name="filename">The name of the file to read</param>
        /// <returns>The file's data, as a string</returns>
        public string ReadText(string filename)
        {
            return encoding.GetString(ReadBytes(filename)).Replace('\r', ' ');
        }

        /// <summary>
        /// Makes all directories along the filepath.
        /// </summary>
        public void MakeDirs(string filepath)
        {
            string fname = BaseDirectory + CleanFileName(filepath) + "/";
            if (!Directory.Exists(fname))
            {
                Directory.CreateDirectory(fname);
            }
        }

        /// <summary>
        /// Writes bytes to a file.
        /// </summary>
        /// <param name="filename">The name of the file to write to</param>
        /// <param name="bytes">The byte data to write</param>
        public void WriteBytes(string filename, byte[] bytes)
        {
            string fname = CleanFileName(filename);
            string dir = Path.GetDirectoryName(BaseDirectory + fname);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(BaseDirectory + fname, bytes);
        }

        /// <summary>
        /// Writes text to a file.
        /// </summary>
        /// <param name="filename">The name of the file to write to</param>
        /// <param name="text">The text data to write</param>
        public void WriteText(string filename, string text)
        {
            WriteBytes(filename, encoding.GetBytes(text.Replace('\r', ' ')));
        }

        /// <summary>
        /// Adds text to a file.
        /// </summary>
        /// <param name="filename">The name of the file to add to</param>
        /// <param name="text">The text data to add</param>
        public void AppendText(string filename, string text)
        {
            string textoutput = ReadText(filename);
            WriteText(filename, textoutput + text);
        }

        /// <summary>
        /// Compresses a byte array using the GZip algorithm.
        /// </summary>
        /// <param name="input">Uncompressed data</param>
        /// <returns>Compressed data</returns>
        public static byte[] GZip(byte[] input)
        {
            MemoryStream memstream = new MemoryStream();
            GZipStream GZStream = new GZipStream(memstream, CompressionMode.Compress);
            GZStream.Write(input, 0, input.Length);
            GZStream.Close();
            byte[] finaldata = memstream.ToArray();
            memstream.Close();
            return finaldata;
        }

        /// <summary>
        /// Decompress a byte array using the GZip algorithm.
        /// </summary>
        /// <param name="input">Compressed data</param>
        /// <returns>Uncompressed data</returns>
        public static byte[] UnGZip(byte[] input)
        {
            using (MemoryStream output = new MemoryStream())
            {
                MemoryStream memstream = new MemoryStream(input);
                GZipStream GZStream = new GZipStream(memstream, CompressionMode.Decompress);
                GZStream.CopyTo(output);
                GZStream.Close();
                memstream.Close();
                return output.ToArray();
            }
        }
    }

    public class PakkedFile
    {
        public string Name = null;

        public bool IsPakked = false;

        public int PakIndex = -1;

        public ZipStorer.ZipFileEntry Entry;

        public PakkedFile(string name)
        {
            Name = name;
        }

        public PakkedFile(string name, int index, ZipStorer.ZipFileEntry entry)
        {
            Name = name;
            IsPakked = true;
            PakIndex = index;
            Entry = entry;
        }
    }

    public class PakFile
    {
        public string Name = null;
        public ZipStorer Storer = null;
        public int FileListIndex = 0;
        public PakFile(string name)
        {
            Name = name;
            Storer = ZipStorer.Open(FileHandler.BaseDirectory + name, FileAccess.Read);
        }
    }
}

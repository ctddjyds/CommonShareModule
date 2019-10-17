public class AutofacUtil
    {
        /// <summary>
        /// Autofac��������
        /// </summary>
        private static IContainer _container;

        /// <summary>
        /// ��ʼ��autofac
        /// </summary>
        public static void InitAutofac()
        {
            var builder = new ContainerBuilder();

            //���ýӿ�����
            builder.RegisterInstance<IDbConnection>(DBFactory.CreateConnection()).As<IDbConnection>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

            //ע��ִ���
            builder.RegisterAssemblyTypes(Assembly.Load("Demo.Repository"))
                   .Where(x => x.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            //����quartz.net����ע��
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new QuartzAutofacJobsModule(Assembly.GetExecutingAssembly()));

            _container = builder.Build();
        }

        /// <summary>
        /// ��Autofac������ȡ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
        }
    }
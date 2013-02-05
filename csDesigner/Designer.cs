using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace csDesigner
{
    public class Designer : DesignSurface
    {
        public Designer()
        {
            Init();
        }

        void Init()
        {
            InitializeNamingService();
            InitializeComponentSerializationService();
            InitializeMenuService();
            InitializeDesignerService();
            IntializeRoot();
        }

        void InitializeNamingService()
        {
            RegisterService(new NamingService(), typeof(INameCreationService));
        }

        void InitializeComponentSerializationService()
        {
            RegisterService(new CodeDomComponentSerializationService(ServiceContainer), typeof (CodeDomComponentSerializationService));
        }

        void InitializeMenuService()
        {
            RegisterService(new MenuCommandService(this), typeof (IMenuCommandService));
        }

        void InitializeDesignerService()
        {
            var designerService = new DesignerService();
            RegisterService(designerService, typeof(DesignerOptionService));
        }

        void RegisterService(object service, Type type)
        {
            ServiceContainer.RemoveService(type, false);
            ServiceContainer.AddService(type, service);
        }

        void IntializeRoot()
        {
            //TODO Need to Create the root component.
        }
    }
}
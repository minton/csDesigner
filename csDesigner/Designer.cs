using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;

namespace csDesigner
{
    public class Designer : DesignSurface
    {
        IDesignerHost _designerHost;

        public Designer()
        {
            Init();
            SetupRoot();
        }

        void Init()
        {
            InitializeNamingService();
            InitializeComponentSerializationService();
            InitializeMenuService();
            InitializeDesignerService();
            InitializeDesignerHost();
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

        void InitializeDesignerHost()
        {
            _designerHost = GetService(typeof (IDesignerHost)) as IDesignerHost;
        }

        void IntializeRoot()
        {
            if (_designerHost == null || _designerHost.RootComponent != null) return;

            BeginLoad(typeof(Form));
            if (LoadErrors.Count != 0)
                throw new Exception("Unknown error occurred loading RootComponent.");
        }

        void SetupRoot()
        {
            RootComponent.FormBorderStyle = FormBorderStyle.None;
            RootComponent.BackColor = Color.MistyRose;
            RootComponent.Dock = DockStyle.Fill;
        }

        public Form RootComponent
        {
            get { return _designerHost.RootComponent as Form; }
        }

        public Control DesignerView
        {
            get { return View as Control; }
        }

        public Control AddControl(Type type, Point location)
        {
            if (_designerHost == null || _designerHost.RootComponent == null) return null;

            var control = _designerHost.CreateComponent(type);
            if (control == null) return null;
            var designer = _designerHost.GetDesigner(control);
            if (designer == null) return null;

            var initializer = designer as IComponentInitializer;
            if (initializer != null) 
                initializer.InitializeNewComponent(null);

            var newControl = control as Control;

            if (newControl != null) 
                newControl.Parent = _designerHost.RootComponent as Control;
            return newControl;
        }
    }
}
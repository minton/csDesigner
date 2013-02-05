using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace csDesigner
{
    internal class NamingService : INameCreationService
    {
        public string CreateName(IContainer container, Type dataType)
        {
            if (container == null) return string.Empty;

            var count = container.Components.OfType<Component>()
                .Where(x => x.GetType() == dataType)
                .Where(x => x.Site.Name.StartsWith(dataType.Name))
                .LongCount();
            return string.Format("{0}{1}", dataType.Name, count == 0 ? "1" : count.ToString());

        }

        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.StartsWith("_") && char.IsLetter(name[0]);
        }

        public void ValidateName(string name)
        {
            if (IsValidName(name)) return;

            throw new ArgumentException(string.Format("{0} is not a valid name."));
        }
    }
}
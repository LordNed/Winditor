using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WindEditor.ViewModel;

namespace WindEditor.Minitors
{
    public interface IMinitor
    {
        MenuItem GetMenuItem();

        void Tick(float DeltaTime);

        void InitModule(WDetailsViewViewModel details_view_model);
        bool RequestCloseModule();
    }
}

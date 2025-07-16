using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public static class GlobalData
    {
        public static BindingList<Tran1Model> Tran1List { get; set; } = new BindingList<Tran1Model>();
       
        public static BindingList<Tran2Model> Tran2List { get; set; } = new BindingList<Tran2Model>();
        public static BindingList<Tran3Model> Tran3List { get; set; } = new BindingList<Tran3Model>();
        public static BindingList<Tran4Model> Tran4List { get; set; } = new BindingList<Tran4Model>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class Tran1Model
    {
       //    [Browsable(false)] // Không cho hiện ID 
     //   public int Id { get; set; }
       

        [DisplayName("STT")] // Chú thích tiếng việt ra Dvg
        public string Id { get; set; }
       [DisplayName("Mô Tả ")] // Chú thích tiếng việt ra Dvg
        public string S1_DC1_Running { get; set; }
        [DisplayName("Vị Trí ")] // Chú thích tiếng việt ra Dvg
        public string Door1_Opening { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }




    }
}

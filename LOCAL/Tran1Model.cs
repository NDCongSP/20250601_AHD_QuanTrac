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
        [DisplayName("STT")]
        public int Id { get; set; }

        [DisplayName("Tên Thiết Bị")]
        public string Device { get; set; }

        [DisplayName("Trạng Thái")]
        public string Status { get; set; }

        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }
    }
}

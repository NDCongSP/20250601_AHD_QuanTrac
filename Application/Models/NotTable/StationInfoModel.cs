using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class StationInfoModel
    {
        /// <summary>
        /// Id của lò Oven, từ 1-->13
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public TagsModel Tags { get; set; }
        /// <summary>
        /// Lưu thông tin tag path, để phục vụ cho sự kiện tagCHanged.
        /// </summary>
        public string Path { get; set; }
    }
}

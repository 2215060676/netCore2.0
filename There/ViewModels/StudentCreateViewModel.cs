using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using There.Model;

namespace There.ViewModels
{
    public class StudentCreateViewModel
    {
        [Display(Name = "名")]//前名显示名
        [DataType(DataType.Text)]//前台显示框
        [Required] //必填
        public string FirstName { get; set; }

        [Display(Name = "姓")]
        [DataType(DataType.Password)]
        [Required, MaxLength(10)]  //必填 最大长度是10
        public string LastName { get; set; }

        [Display(Name = "性别")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "出生日期")]
        public Gender Gender { get; set; }
    }
}

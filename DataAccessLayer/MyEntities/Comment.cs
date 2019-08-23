using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.MyEntities
{
   public class Comment:BaseEntity
    {
        public string CommentStr { get; set; }

        public virtual User User { get; set; }

        public virtual Cake Cake { get; set; }
    }
}

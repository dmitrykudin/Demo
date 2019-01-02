using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class ClassTagRepository : IClassTagRepository
    {
        public ClassTag CreateClassTag(int classId, int tagId)
        {
            using (var cc = new CustomersContext())
            {
                ClassTag classTag = cc.ClassTags.Add(new ClassTag()
                {
                    ClassId = classId,
                    TagId = tagId
                });
                cc.SaveChanges();
                return classTag;
            }
        }

        public void DeleteClassTag(int tagId)
        {
            //TODO
            throw new NotImplementedException();
        }

        public ClassTag GetClassTag(int tagId)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}

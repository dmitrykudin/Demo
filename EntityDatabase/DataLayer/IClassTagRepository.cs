using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IClassTagRepository
    {
        ClassTag CreateClassTag(int classId, int tagId);
        ClassTag GetClassTag(int tagId);
        void DeleteClassTag(int tagId);
    }
}

using MovieCollectionDAL.Entities;

namespace MovieCollectionDAL.Repositories
{
    public interface IAudienceRepository
    {
        bool Create(Audience a);
        bool Update(Audience a);
    }
}
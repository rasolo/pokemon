namespace Pokemon.Api.Core.Services
{
    public interface IPokemonService
    {
        string GetFilteredSortQuery(string sortQuery);
        bool NameIsUnique(string name);
    }
}
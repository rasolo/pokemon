namespace Pokemon.Core.Contracts
{
  public interface IPokemonRepository
    {
        Entities.Pokemon GetByName(string name);
    }
}

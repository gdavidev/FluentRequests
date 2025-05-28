using Examples.PokeAPIClient.Data;
using FluentRequests;

namespace Examples.PokeAPIClient.API
{
    public class PokemonAPI
    {
        public IEnumerable<Pokemon> GetPokemons(int page)
        {
            var pokemons = Requests.Get<IEnumerable<Pokemon>>()
        }
    }
}

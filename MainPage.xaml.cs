using EstadosCidades.Models;

namespace EstadosCidades
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Carregar estados e cidades na inicialização
            CarregarCidades();
        }

        private async void CarregarCidades()
        {
            // Simular requisição à API Brasil para listar cidades por estado
            List<Cidade> cidades = await BuscarCidadesDoEstado("SP");

            PickerCidade.ItemsSource = cidades.Select(c => c.Nome).ToList();
        }

        private async void OnCidadeSelecionada(object sender, EventArgs e)
        {
            string cidadeSelecionada = (string)PickerCidade.SelectedItem;
            // Buscar previsão para o dia atual
            Previsao previsaoAtual = await BuscarPrevisao(cidadeSelecionada);

            LabelPrevisaoAtual.Text = $"Previsão para {cidadeSelecionada}: {previsaoAtual.ClimaProximosDias.First().CondicaoDesc}";
            // Atualizar a previsão para os próximos 6 dias
            ListaPrevisao.ItemsSource = previsaoAtual.ClimaProximosDias.Skip(1);
        }

        private async Task<List<Cidade>> BuscarCidadesDoEstado(string estado)
        {
            // Simular chamada à API Brasil para listar as cidades do estado
            return new List<Cidade>
        {
            new Cidade { Nome = "São Paulo", Estado = "SP", Id = 1 },
            new Cidade { Nome = "Campinas", Estado = "SP", Id = 2 },
            new Cidade { Nome = "Santos", Estado = "SP", Id = 3 }
        };
        }

        private async Task<Previsao> BuscarPrevisao(string nomeCidade)
        {
            // Simular chamada à API para buscar a previsão do tempo
            return new Previsao
            {
                Cidade = new Cidade { Nome = nomeCidade, Estado = "SP", Id = 1 },
                AtualizadoEm = DateTime.Now,
                ClimaProximosDias = new List<Clima>
            {
                new Clima { Data = DateTime.Today, Condicao = "Sol", Min = 20, Max = 30, IndiceUV = 8, CondicaoDesc = "Ensolarado" },
                new Clima { Data = DateTime.Today.AddDays(1), Condicao = "Chuva", Min = 18, Max = 25, IndiceUV = 5, CondicaoDesc = "Chuvas rápidas" },
                // Mais dias aqui...
            }
            };
        }
    }

}

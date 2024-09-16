# DesafioBackEndProject

Bem-vindo ao **DesafioBackEndProject**! Este documento irá guiá-lo na configuração do ambiente e na execução do projeto.

## Requisitos

- [Docker](https://www.docker.com/get-started) (para executar o ambiente)
- [Docker Compose](https://docs.docker.com/compose/install/) (para orquestrar os contêineres)
- [.NET SDK](https://dotnet.microsoft.com/download) (para executar o projeto)
- [Insomnia](https://insomnia.rest/) (para testar a API)

## Configuração do Ambiente

1. **Clone o repositório**

   Primeiro, clone o repositório para o seu ambiente local:

   ```bash
   git clone https://github.com/LuigiTavolaro/Desafio-BackEnd/tree/feature/desafio
   cd DesafioBackEndProject

### 2. Suba os contêineres Docker

No diretório do repositório, execute o comando abaixo para construir e iniciar os contêineres Docker:

```bash
docker-compose up --build

Este comando irá construir as imagens e iniciar os serviços definidos no arquivo docker-compose.yml.

### 3. Execute o projeto .NET

Com os contêineres em execução, inicie o projeto .NET `DesafioBackEndProject`. Navegue até o diretório do projeto e execute:

```bash
dotnet run

Certifique-se de que o projeto está configurado para se conectar ao banco de dados e ao RabbitMQ definidos no Docker Compose.

## Testando a API

Para testar a API, você pode usar a coleção do Insomnia que está disponível no repositório. A coleção inclui exemplos de requisições para a API.

### 1. Importar a coleção no Insomnia

- Abra o Insomnia.
- Clique em "Import" no menu.
- Selecione "Import From File" e escolha o arquivo de coleção do Insomnia que está no repositório.

### 2. Executar requisições

Com a coleção importada, você pode começar a executar as requisições para testar os endpoints da API. Certifique-se de que os contêineres Docker estão funcionando e o projeto .NET está em execução.

## Estrutura do Repositório

- `docker-compose.yml`: Arquivo de configuração para Docker Compose.
- `DesafioBackEndProject/`: Diretório contendo o código fonte do projeto .NET.
- `insomnia_collection/`: Diretório contendo a coleção do Insomnia para testar a API.
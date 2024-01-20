<h1 align="left">Tech Challenge 02 - Azure Function + CI/CD Pipeline - FIAP 2023</h1>
O <b>[Tech Challenge - Fase 02]</b> do curso de Arquitetura de Sistemas .Net com Azure da FIAP se trata da construção de uma Azure Function para simulação de uma ordem de pedido e a documentação/passo a passo da criação de uma pipeline com CI/CD utilizando Azure DevOps.

<h3 align="left">Integrantes</h3>
- 🎮 <a href="https://github.com/talles2512">Hebert Talles de Jesus Silva</a> (RM352000)</br>
- 🕹️ <a href="https://github.com/LeonardoCavi">Leonardo Cavichiolli de Oliveira</a> (RM351999)

<h3 align="left">Projeto</h3>
- 👾 TC2_FNC_OrdemServico_FIAP (FNC_OrdemServico)

<h4 align="left">Projeto - FNC_OrdemServico</h4>
Azure Function desenvolvida em .NET 6 Core utilizando a IDE Visual Studio 2022, utilizando EF Core para gestão de dados (SQL Server Local ou Nuvem-PaaS). A function simula a ordem de pedido de manutenção de produtos voltados para hardwares antigos e novos de video games. A function foi desenvolvida utilizando o template durable, contendo assim um orquestador e alguns functions que tem funcionalidade bem definidas como.:
</br>
</br>
- 👾 <b>HttpTriggerFunction.:</b> Function responsável por receber o "acionamento" http, validando o corpo da requisição e inicializando o Orquestrador caso a validação tenha sucesso.</br>
- 👾 <b>OrdemOrchestrator.:</b> Orquestrador da function, responsável por chamar as functions especificas do projeto e retornar o valor da ordem de serviço e status da ordem.</br>
- 👾 <b>OrdemBancoFunction.:</b> Serve como a camada de intermediação com o banco de dados para a Ordem, responsável por obter ou inserir uma nova ordem conforme a regra de negócio.</br>
- 👾 <b>ProcessamentoOrdemBancoFunction.:</b> Serve como a camada de intermediação com o banco de dados para o Processamento da Ordem, inserindo e atualizando o andamento da Ordem de acordo com o processamento interno.</br>
- 👾 <b>VerificaTipoProdutoFunction.:</b> Função responsável por verificar se a Marca do Produto, Tipo e Modelo são fazem parte da cobertura do serviço, calculando o tempo de garantia do mesmo.</br>
- 👾 <b>GeraPrazoManutencaoFunction.:</b> Função responsável por verificar o tipo de defeito informado da Ordem, identificando se há cobertura e devolvendo o prazo de manutenção/conclusão do serviço em dias úteis.</br>
- 👾 <b>VerificaGarantiaProdutoFunction.:</b> Função responsável por verificar, de acordo com a data de aquisição do produto informado e da cobertura de serviço processada, se o serviço a ser realizado estara coberto pela garantia ou não.</br>

<h4 align="left">Instruções do projeto - Preparação</h4>
A configuração para execução da Azure Function se trata apenas de qual local você pretende executa-la. Seja localmente ou subindo o serviço no Azure Function na Nuvem (vídeo demonstrativo CI/CD em andamento).:

- 👾 <b>Conexão com o banco de dados.:</b> Acesse o arquivo local.settings.json no projeto FNC_OrdemServico e altere o valor do parametro "OrdemServicoDbSecret", esse valor aceita tanto uma string de conexão de um banco como um Secret (Azure Key Vault) de uma string de conexão de banco também.</br>
- 👾 <b>Scripts de banco de dados.:</b> Segue os scripts de banco para criação do database e as tabelas necessárias para execução do projeto - <a href="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Scripts%20FNC_OrdemServico/ScriptSQL_FNC_OrdemServico.sql">Script SQL Tabelas - FNC_OrdemServico</a>.</br>
    - 👾 <b>Diagrama da Tabelas.:</b> <a href="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Scripts%20FNC_OrdemServico/Diagrama_FNC_OrdemServico_Tabelas.png">Diagramas SQL</a>.

<h4 align="left">Iniciando o projeto</h4>
Ao executar a Azure Function seja local ou seja em nuvem, podemos utilizar como aplicação cliente o Postman para realizar alguns requisições HTTP para function. Segue alguns prints de exemplo (<a href="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/tree/develop/Documentos%20Uteis/Json%20Exemplo">requisições .jsons</a>).:

<img src="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Prints%20Uteis/01%20-%20Function%20Rodando%20no%20Console%20Windows.png"></img>
<img src="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Prints%20Uteis/02%20-%20Prepara%C3%A7%C3%A3o%20da%20Requisi%C3%A7%C3%A3o%20para%20o%20HttpTriggerFunction%20no%20Postman.png"></img>
<img src="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Prints%20Uteis/03%20-%20Retorno%20da%20Requisi%C3%A7%C3%A3o%20do%20FNC_OrdemServico.png"></img>
<img src="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Prints%20Uteis/04%20-%20Verificando%20o%20statusQueryGetUri%20da%20Ordem%20enviada.png"></img>
<img src="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Prints%20Uteis/05%20-%20Verificando%20o%20Registro%20da%20Ordem%20enviada%20no%20Banco%20de%20Dados.png"></img>

<h4 align="left">Dados necessários para teste</h4>
Segue a lista de marcas do produto, tipos, modelos e tipos de defeitos aceitos na requisições de novas ordens.: <a href="https://github.com/talles2512/TC2_FNC_OrdemServico_FIAP/blob/develop/Documentos%20Uteis/Dados%20Necessarios%20para%20Emissao%20de%20Ordem/readme.md">Documentos de Dados para Requisições no FNC_OrdemServico</a>.

<h3 align="left">Criação de Pipeline CI/CD da Azure Fuction</h3>
[Em Construção...]

<h3 align="left">Comandos utilizados na construção do Pipeline</h3>

- Criar a Storage Account:</br>
    - az storage account create --name [nome-do-storage-account] --resource-group [nome-do-grupo-de-recursos] --location brazilsouth --sku Standard_LRS</br>
- Criar a Function App:</br>
    - az functionapp create --resource-group [nome-do-grupo-de-recursos] --name [nome-da-function] --consumption-plan-location brazilsouth --os-type Linux --runtime dotnet --functions-version 4 --storage-account [nome-do-storage-account]</br>
- Habilitar o Identity na Function App:</br>
    - az functionapp identity assign --n [nome-da-function] --resource-group [nome-do-grupo-de-recursos]</br>
- Obter PrincipalId da Function App e atribuir a uma variável:</br>
    - $functionPrincipalId = (az functionapp show -n [nome-da-function] -g [nome-do-grupo-de-recursos] --query identity.principalId --out tsv)</br>
- Dar permissão para a Function App ler secrets no Key Vault:</br>
    - az keyvault set-policy --name [nome-do-key-vault] --object-id $functionPrincipalId --secret-permissions get</br>
- Obter Id do Secret (Url) no Key Vault e atribuir a uma variável:</br>
    - $secretId = (az keyvault secret show -n [nome-do-secret] --vault-name [nome-do-key-vault] --query id --out tsv)</br>
- Adicionar Parâmetro de referência ao Secret no AppSettings da Function App:</br>
    - az functionapp config appsettings set --name [nome-da-function] --resource-group [nome-do-grupo-de-recursos] --settings OrdemServicoDbSecret=\`""@Microsoft.KeyVault(SecretUri=$secretId)"`"</br>

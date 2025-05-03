# Aplicação desenvolvida pra teste.

O projeto base usa um template de API RESTful, que já possui a estrutura básica de uma API,
com o padrão CQRS e Repository Pattern.

Algumas características desse template (projeto como um todo) são:
- Construido usando o .NET 8.0
- Utiliza o EntityFramework Core como ORM
- Seque o [CQRS Pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs) e [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- Utiliza o PostgreSQL como banco de dados.
- Utiliza o FluentValidation para validação de dados
- Utiliza o MediatR para implementar o padrão CQRS
- Utiliza XUnit, Bogus e FluentAssertions para criar os testes

## Funcionalidades implementadas no projeto:
- Cadastro de usuários
- Login de usuários
- Cadastro de produtos
- Cadastro de venda (a partir de produtos somente por usuários logados e com função específica)
- Listagem de produtos
- Listagem de vendas
- Alteração de produtos
- Alteração de venda
- Exclusão de produtos
- Exclusão de venda

## FrontEnd
- O FrontEnd foi desenvolvido usando BlazorWebAssembly.

## Funcionalidades que serão implementadas (não necessáriamente nessa ordem):
- [X] Implementar exclusão lógica de usuários e dados de produtos e vendas.
- [X] Implementar o cancelamento de uma venda.
- [X] Criar a opção de carrinho de compra, fazendo com que o usuário logado possa escolher mais de um produto para venda.
- [X] ✨ Implementar paginação de Produtos e Vendas
- [X] 💡 Adicionar o usuário que criou uma venda.
- [X] 💡 Possibilitar que usúario do tipo Customer possa criar vendas, mas não editar ou remover.
- [ ] Adição de listagem, edição e cadastro de funcionários
- [ ] Adição de listagem, edição e cadastro de fornecedores.
- [ ] Adição de Categorias para os produtos.
- [ ] Adição de cadastro de clientes, juntamente com endereço de entrega.
- [ ] - [ ] 🔧 Justar Data e Hora de cadastro e modificação de produtos e vendas.
- [ ] Adicção de entregas, com possibilidade de ver entregas em andamento (simuladas), concluídas e planejadas além de poder fazer o planejamento de entregas
- [ ] Adição de menu de relatórios, podendo ver relatórios de vendas,estoque e entregas.

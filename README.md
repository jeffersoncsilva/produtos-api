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
- [ ] ✨ Implementar paginação de Produtos e Vendas
- [ ] 🔧 Justar Data e Hora de cadastro e modificação de produtos e vendas.
- [ ] Criar a opção de relatório de produtos, onde o usuário logado possa ver os produtos cadastrados, podendo filtrar por período, categoria e os produtos que ele mesmo cadastrou.
- [ ] Criar a opção de relatório de vendas, onde o usuário logado possa ver as vendas que ele realizou e possa filtrar por período.
- [ ] Adicionar novas funcionalidades para criaçao de usuários (adição de cargos)
- [ ] Remover do cadastro a opção de escolha de cargo no cadastro.
- [ ] Implementar um pré-cadastro, onde um usuário autenticado possa iniciar o cadastro de um novo usuário, inserindo o seu e-mail e este recebendo um link para continuar o seu cadastro com o restante de suas informações.
- [ ] Adicionar autenticação multifator (MFA) para login de usuários.
- [ ] Adicionar a opção de recuperação de senha, onde o usuário solicita um link para redefinição de senha enviado para o e-mail cadastrado.
- [ ] Implementar um sistema de entregas, onde dado o endereço a venda será entregue no endereço cadastrado.
- [ ] Criar a opção de pagamento, onde o usuário logado possa escolher a forma de pagamento (cartão, dinheiro, etc)
- [ ] Criar a opção de relatório de usuários, onde o usuário logado possa ver os usuários cadastrados em um determinado período, filtrar por quantidade de compras.
- [ ] Criar a opção de relatório de vendas por usuário, onde o usuário logado possa ver as vendas realizadas por ele em um determinado período
- [ ] Criar a opção de relatório de vendas por produto, onde o usuário logado possa ver as vendas realizadas por produto em um determinado período
- [ ] Criar a opção de relatório de vendas por forma de pagamento, onde o usuário logado possa ver as vendas realizadas por forma de pagamento em um determinado período





# Projeto API - ASP.NET Core

## Arquitetura

O projeto segue a arquitetura *MVC (Model-View-Controller)*, que divide a aplicação em três camadas principais:

- *Model*: Responsável pela camada de dados e regras de negócio.
- *View*: A interface do usuário, que exibe os dados e interage com o usuário.
- *Controller*: Lida com as requisições do usuário, interage com o Model e retorna a View apropriada.

Essa arquitetura facilita a manutenção, organização e escalabilidade do projeto.

## Design Patterns Utilizados

- *Singleton*: O padrão Singleton foi implementado na classe MeuServicoSingleton. Ele garante que apenas uma instância dessa classe seja criada e utilizada durante toda a aplicação. Isso é útil para gerenciar estados ou configurações globais que precisam ser compartilhados consistentemente em todo o sistema.

## Banco de Dados

Utilizamos o *Oracle* como sistema de gerenciamento de banco de dados. Todas as informações capturadas pela aplicação são armazenadas nele, garantindo robustez e confiabilidade na gestão dos dados.

## Documentação e Testes

Para documentar e testar a API, utilizamos o *Swagger*. Ele facilita a criação de uma documentação interativa, permitindo a execução de testes diretamente na interface gerada.

Para acessar a documentação interativa da API, basta acessar o endpoint /swagger após rodar a aplicação.


## Integrantes do Grupo

- *Leonardo Yuuki Nakazone* - RM: 550373
- *Leonardo Blanco Pérez Ribeiro* - RM: 99119
- *Paulo Henrique Luchini Ferreira* - RM: 98082
- *Gustavo Moreira Gonçalves* - RM: 97999
- *Daniel Soares Delfin* - RM: 552184

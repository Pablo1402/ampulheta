# Ampulheta API

O [Ampulheta] disponibiliza uma API RESTful que fornece os endpoints necessários para criação de um aplicativo de apontamento de horas em projetos.

Recursos disponíveis para acesso via API:
* [**Gerenciamento de projetos**]
* [**Gerenciamento de usuarios**]
* [**Controle de acesso por permissões**]
* [**Apontamento de horas em projetos**]

## Métodos
Requisições para a API devem seguir os padrões:
| Método | Descrição |
|---|---|
| `GET` | Retorna informações de um ou mais registros. |
| `POST` | Utilizado para criar um novo registro. |
| `PUT` | Atualiza dados de um registro. |

## Respostas

| Código | Descrição |
|---|---|
| `200` | Requisição executada com sucesso (success).|
| `400` | Erros de validação ou os campos informados não existem no sistema.|
| `401` | Dados de acesso inválidos.|
| `401` | Sem autorização de acesso ao recurso solicitado.|
| `404` | Registro pesquisado não encontrado (Not found).|

## Solicitando tokens de acesso
### Utilizando token [POST]

O `token` é do formato JWT e contém informações do usuário. Este é o token utilizado em todos os serviços disponibilizados pela api, exceto o serviço de autenticação.
O token deve ser enviado nas requisiçoes como `bearer`.


# Recursos

# Autenticação [POST /api/v1/Auth]

+ Request (application/json)
{
  "login": "admin",
  "password": "admin"
}

+ Response 200 (application/json)
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiQURNSU4iLCJuYmYiOjE2ODIyOTE3MzksImV4cCI6MTY4MjI5ODkzOSwiaWF0IjoxNjgyMjkxNzM5fQ.ElEDoL1hV26aGOHtclsp29mz3xkfTzF6cdXf38xEa5U",
  "user": {
    "id": 2,
    "name": "user admin",
    "login": "admin",
    "email": "barretopablo1991@gmail.com"
  }
}


# Projeto 

## Buscar projetos [GET api/v1/Projetc?PageIndex=1&PageSize=10]

+ Request
PageIndex - default 1
PageSize - default 10 

+ Response 200 (application/json)
[
  {
    "id": 1,
    "name": "projeto vibbra",
    "note": "api para criacao aplicativo de apontamentos de horas"
  },
  {
    "id": 2,
    "name": "projeto teste",
    "note": "teste de paginacao"
  }
]

## Salvar projeto [POST api/v1/Projetc]

+ Request (application/json)
{
  "name": "apontamento de horas",
  "note": "projeto para apontamentos de horas"
}

+ Response 200 (application/json)
{
  "id": 3,
  "name": "apontamento de horas",
  "note": "projeto para apontamentos de horas"
}

## Buscar projeto por identificador [GET api/v1/Projetc/3]

+ Response 200 (application/json)
{
  "id": 3,
  "name": "apontamento de horas",
  "note": "projeto para apontamentos de horas"
}

## Atualizar projeto [PUT api/v1/Projetc/3]

+ Request (application/json)
{
  "id": 3,
  "name": "apontamento de horas",
  "note": "projeto para apontamentos de horas"
}

+ Response 200 (application/json)
{
  "id": 3,
  "name": "apontamento de horas",
  "note": "projeto para apontamentos de horas"
}


# Apontamentos 

## Criar Apontamento [POST api/v1/Time]

+ Request (application/json)
{
  "userId": 3,
  "projectId": 3,
  "startedAt": "2023-04-23T23:30:39.537Z",
  "endedAt": "2023-04-23T23:38:39.537Z"
}

+ Response 200 (application/json)
{
  "id": 10,
  "userId": 3,
  "projectId": 3,
  "startedAt": "2023-04-23T23:30:39.537Z",
  "endedAt": "2023-04-23T23:38:39.537Z"
}

## Atualizar Apontamento [PUT api/v1/Time]

+ Request (application/json)
{
  "id": 10,
  "userId": 3,
  "projectId": 3,
  "startedAt": "2023-04-23T23:30:39.537Z",
  "endedAt": "2023-04-23T23:40:39.537Z"
}

+ Response 200 (application/json)
{
  "id": 10,
  "userId": 3,
  "projectId": 3,
  "startedAt": "2023-04-23T23:30:39.537Z",
  "endedAt": "2023-04-23T23:38:39.537Z"
}


## Buscar Apontamentos por projeto [GET api/v1/Time/3]

+ Response 200 (application/json)
[
  {
    "id": 10,
    "userId": 3,
    "userName": "Pablo Barreto teste",
    "projectId": 3,
    "startedAt": "2023-04-23T23:30:39.537",
    "endedAt": "2023-04-23T23:40:39.537"
  }
]


# Usuarios

## Buscar Usuarios [GET /api/v1/User?PageIndex=1&PageSize=10]

+ Request
PageIndex - default 1
PageSize - default 10 

+ Response 200 (application/json)
[
  {
    "id": 2,
    "name": "user admin",
    "login": "admin",
    "email": "barretopablo1991@gmail.com"
  }
]

## Salvar Usuarios [POST /api/v1/User]

+ Request (application/json)
{
  "name": "Usuario readme",
  "email": "readme@gmail.com.br",
  "login": "read.me",
  "password": "R&@d.m3"
}

+ Response 200 (application/json)
{
  "id": 5,
  "name": "Usuario readme",
  "login": "read.me",
  "email": "readme@gmail.com.br"
}


## Atualizar Usuarios [PUT /api/v1/User]

+ Request (application/json)
{
  "id": 5,
  "name": "Usuario readme",
  "login": "read.me",
  "email": "readme@gmail.com.br",
  "password":"R3@d.m3"
}

+ Response 200 (application/json)
{
  "id": 5,
  "name": "Usuario readme",
  "login": "read.me",
  "email": "readme@gmail.com.br"
}


### Pré-requisitos

+ Banco de dados sql-server
+ visual studio 2022
+ SDK .net6

### 🎲 Rodando a API
Para rodar o projeto sera necessário apenas ter o projeto baixado e sql-server, visual studio 2022 e o sdk .net6 instalados na máquina.
O projeto está configurado para criar o banco e inserir as informações necessárias para o projeto ser executado.
Será criado um usuário com perfil ADMIN e com crendenciais (password: admin, login: admin) para que se possa fazer os testes iniciais.



### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [.net6]
- [c#]
- [Sql-Server]
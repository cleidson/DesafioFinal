# DesafioFinal

## 📌 Visão Geral
O **DesafioFinal** é um projeto desenvolvido em **C# com ASP.NET 9**, seguindo princípios da **Clean Architecture** para garantir modularidade e escalabilidade. A aplicação utiliza **Entity Framework** para persistência dos dados em **PostgreSQL**, sendo executada de forma automatizada com **Docker Compose** no **Visual Studio 2022**.

## 🏗️ Arquitetura do Projeto
A estrutura do projeto foi organizada em camadas para garantir separação de responsabilidades:

### 📂 **01-Domains (Camada de Domínio)**
🔹 `DesafioFinal.Core`
- **DTOs**: Modelos de transferência de dados.
- **Logic**: Contém as interfaces dos serviços e regras de negócio.
- **Models**: Entidades do domínio.
- **Services**: Implementações das regras de negócio, como `ClienteService.cs`.

### 📂 **02-Infrastructure (Camada de Infraestrutura)**
🔹 `DesafioFinal.Infrastructure`
- **Data**: Configuração do banco de dados e contexto do EF.
- **Repositories**: Implementação do padrão Repository para acesso aos dados.
- **Migrations**: Scripts de migração do banco de dados.
- **ExceptionHandler**: Tratamento centralizado de erros.

### 📂 **03-Presentation (Camada de Apresentação)**
🔹 `DesafioFinal.Api`
- Implementa a API seguindo o padrão MVC.
- Exposição dos endpoints REST para comunicação com o frontend ou consumidores externos.

### 🐳 **Docker Compose**
- Configurado para levantar o ambiente automaticamente no **Visual Studio 2022**, incluindo o banco de dados PostgreSQL e a API.

## 🚀 Tecnologias Utilizadas
✅ **C# com ASP.NET 9** – Framework para desenvolvimento backend.  
✅ **Entity Framework Core** – ORM para interação com PostgreSQL.  
✅ **PostgreSQL** – Banco de dados relacional para persistência de dados.  
✅ **Docker Compose** – Gerenciamento do ambiente de desenvolvimento.  
✅ **Clean Architecture** – Organização do código em camadas bem definidas.  

## 🎯 Como Executar o Projeto

### **1️⃣ Pré-requisitos**
Certifique-se de ter instalado:
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/downloads/)

### **2️⃣ Clonar o Repositório**
```sh
  git clone https://github.com/seu-usuario/desafiofinal.git
  cd desafiofinal
```

### **3️⃣ Executar com Docker**
```sh
  docker-compose up -d
```
Isso irá subir todos os contêineres necessários para rodar a aplicação.

### **4️⃣ Acessar a API**
A API estará disponível em:  
🔗 `http://localhost:5000/swagger` (caso tenha Swagger configurado)

## 📌 Contribuição
Caso queira contribuir com melhorias no projeto, siga os passos:
1. Faça um **Fork** do repositório.
2. Crie uma **Branch** para a sua feature (`git checkout -b minha-feature`).
3. Faça as alterações e **commite** (`git commit -m 'Minha contribuição'`).
4. Faça o **Push** (`git push origin minha-feature`).
5. Abra um **Pull Request**.

---
🚀 **DesafioFinal** – Desenvolvido com **C# e ASP.NET 9** utilizando **PostgreSQL** e **Docker**!

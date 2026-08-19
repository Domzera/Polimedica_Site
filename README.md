# Polimédica do Vale — Sistema de Catálogo e Vendas

Sistema web para catálogo de produtos e futura loja online da **Polimédica do Vale**, desenvolvido em **ASP.NET Core 9 (MVC)** com arquitetura em camadas.

## ✨ Funcionalidades

- Cadastro e gestão de produtos, categorias e marcas
- Autenticação e autorização de usuários com **ASP.NET Core Identity**
- Upload e gerenciamento de imagens via **Cloudinary**
- Banners promocionais e sistema de promoções
- Seção "Ter em Casa"
- Estrutura preparada para carrinho de compras, pedidos, pagamentos, endereços e cupons de desconto
- Avaliações de produtos

## 🛠️ Stack Técnica

| Camada | Tecnologia |
|---|---|
| Backend | ASP.NET Core 9 (MVC) |
| ORM | Entity Framework Core 9 |
| Banco de dados | SQL Server |
| Autenticação | ASP.NET Core Identity |
| Armazenamento de imagens | CloudinaryDotNet |
| Frontend | Bootstrap 5, jQuery |
| Containerização | Docker |

## 📁 Estrutura do Projeto

```
├── Controllers/      # Controllers MVC (Produto, Categoria, Marca, Usuario, Banner, Promocao...)
├── Data/              # DbContext, configurações do Cloudinary, enums
├── Interface/         # Contratos dos repositórios e serviços
├── Repository/        # Implementação do acesso a dados
├── Services/          # Serviços auxiliares (ex: PhotoService)
├── Models/            # Entidades de domínio
├── ViewModel/          # ViewModels usados nas Views
├── Views/             # Páginas Razor (.cshtml)
├── Migrations/        # Migrations do Entity Framework
└── wwwroot/           # Arquivos estáticos (imagens, CSS, JS)
```

## 🚀 Como rodar o projeto

### Pré-requisitos
- .NET 9 SDK
- SQL Server (local ou remoto)
- Conta no Cloudinary (para upload de imagens)

### Passos

1. Clone o repositório
2. Configure a connection string em `appsettings.json` (`DefaultConnection`)
3. Configure as credenciais do Cloudinary em `CloudinarySettings`
4. Rode as migrations:
   ```
   Add-Migration NomeDaMigration
   Update-Database
   ```
5. Rode o projeto (Visual Studio 2022 ou `dotnet run`)

### Rodando com Docker

```bash
docker build -t polimedica-site .
docker run -p 8080:8080 polimedica-site
```

## 🗺️ Roadmap

- [ ] Finalizar fluxo de carrinho de compras e checkout
- [ ] Integração de pagamentos
- [ ] Painel administrativo completo
- [ ] Deploy em produção

## 📌 Status

Em desenvolvimento ativo — projeto pessoal em transição de catálogo institucional para loja de vendas online.

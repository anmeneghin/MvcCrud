# MvcCrud
## MvcCrud — Guia rápido para rodar localmente

Descrição
---------
Projeto de exemplo com CRUD para uma entidade Product usando ASP.NET Core (Views + Controllers) e Entity Framework Core. Ideal para estudos — atualmente usa um provedor InMemory para persistência em memória.

Pré-requisitos
-------------
- .NET SDK 8.x instalado. Verifique com:
``dotnet --version dotnet --list-sdks``

- (Opcional) Visual Studio com suporte a .NET 8. Use __Help > Check for Updates__ e instale workloads de ASP.NET se necessário.

Estrutura principal (onde olhar)
-------------------------------
- Models/Product.cs — modelo da entidade.
- Data/ApplicationDbContext.cs — DbContext do EF Core.
- Controllers/ProductsController.cs — ações CRUD.
- Views/Products/*.cshtml — views Index/Create/Edit/Details/Delete.
- Views/Shared/_Layout.cshtml — layout e scripts comuns.
- Program.cs — configuração do app (serviços, DbContext, cultura).

Instalação e execução
--------------------
1. Abra um terminal na raiz do projeto.
2. Restaurar dependências e compilar:
   ``dotnet restore dotnet build``
3. Se necessário, adicione o provedor InMemory (usado neste exemplo):
   ``dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.*``
4. Rodar a aplicação:
   ``dotnet run``
   Abra o navegador em http://localhost:5000 (ou na URL exibida no console). A rota padrão aponta para `/Products`.

No Visual Studio
- Abra o projeto e pressione F5 ou use __Debug > Start Debugging__.

Cultura e entrada decimal (pt-BR)
-------------------------------
- Para aceitar vírgula como separador decimal (ex.: `2,50`), a aplicação deve configurar a cultura para `pt-BR` em Program.cs (ex.: UseRequestLocalization / CultureInfo.DefaultThreadCurrentCulture).
- As views de Create/Edit usam scripts que:
  - convertem ponto para vírgula na interface,
  - limitam a entrada a 2 casas decimais enquanto o usuário digita,
  - sobrescrevem a validação do jQuery Validate para aceitar vírgula.
- Se aparecer "The field Price must be a number.":
  1. Confirme que Program.cs define cultura `pt-BR`.
  2. Verifique se `_ValidationScriptsPartial` é carregado antes do override do método `number` do jQuery Validate (o override deve estar dentro de `@section Scripts` depois do partial).
  3. Use `input` com `type="text"` + `inputmode="decimal"` (evita validação nativa do navegador que exige ponto).

Atualizar / usar EF Core correto
-------------------------------
- Para compatibilidade com .NET 8, use Entity Framework Core 8.x:
  ``dotnet tool install --global dotnet-ef --version 8.* dotnet ef migrations add Initial dotnet ef database update``

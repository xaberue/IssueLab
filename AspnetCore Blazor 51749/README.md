# AspnetCore Issue 51749 

## Blazor SSR error when rendermode is InteractiveWebAssembly

This repository contains a minimal repro project to analyse the issue.


### Steps

1. Open IssueLab.Blazor.WebApi, and run it (F5)
2. Open IssueLab.Blazor.Ssr, and run it (F5)
3. In the Blazor web page  opened after step 2, navigate to `Users`
4. After a few seconds, the crash will appear


### Notes
- The project is using also QuickGrid
- If I remove the rendermode in Users.razor, it will work, but of course without enabling the requried user interactivy. `@rendermode="RenderMode.InteractiveWebAssembly"`
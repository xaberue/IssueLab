# IdentityServer configuration with Scalar

## Description

This repository contains a minimal repro project to analyse the scenario and why it's failing.


### Steps

1. In order to seed the database with test users, in launchSettings.json for IdentityServer service, remove the `--` and it will automatically seed the database.1. Run the Aspire project.
2. Stop the project, remove the param or restore the `--`.
3. Run the Aspire project again , it contains both IdentityServer and ab API protected with IdentityServer.
4. From the Aspire dashboard, Open Scalar UI for the Weather API, and click on Authorise.
5. It will redirect to IdentityServer, where you can login with the test user (alice/Pass123$).
6. Nothing will happen even though the user has been loged in. 
    1. The user is loged in the web, it appears in the menu bar from IdP.
    2. If you see logs, you will notice that there's one from IdP log saying `Showing login: User is not active`
    3. There's no redirection or action that allows Scalar UI to be authenticated.


### Links
- [Discusiopn opened in Duende's IdentityServer Github project](https://github.com/orgs/DuendeSoftware/discussions/533
- [Discusiopn opened in Scalar Github project](https://github.com/scalar/scalar/discussions/8958)
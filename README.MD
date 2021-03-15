# arkopongBack
This project is SignalR backend for [ScreenArkoPong][Game]

[Game repo]: https://github.com/RTUITLab/Screen-ArkoPong
## Requirements
* .NET 5.0

---
## Start using .Net
* Open `\arkopongBack` folder in the console;
* Use `dotnet run` command;
* Wait.
---

## Start Docker

1. Build project
    ```bash
    dotnet tool restore
    dotnet nuke
    ```
2. Build docker image
    ```bash
    docker build -t rtuitlab/screen-arkopong-back ./deploy
    docker run -it --rm -p 5501:5501 rtuitlab/screen-arkopong-back
    ```

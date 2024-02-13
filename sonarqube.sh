dotnet sonarscanner begin /k:"dotnet-ci" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_6ac357da33b7455955e336011a67f32cf15b6d81" /d:sonar.cs.opencover.reportsPaths=coverage.xml
dotnet clean
dotnet build --no-incremental
coverlet ./test/Domain.Test/bin/Debug/net6.0/Domain.Test.dll --target "dotnet" --targetargs "test --no-build" -f=opencover -o="coverage.xml"
dotnet sonarscanner end /d:sonar.token="sqp_6ac357da33b7455955e336011a67f32cf15b6d81"
# Project Variables
PROJECT_NAME ?= SolarCoffee
ORG_NAME ?= SolarCoffee
REPO_NAME ?= SolarCoffee

.PHONY: migrations db run watchrun

migrations: 
	cd SolarCoffee.Data && dotnet ef --startup-project ../SolarCoffee.Web migrations add $(mname) & cd ..

db:
	cd SolarCoffee.Data && dotnet ef --startup-project ../SolarCoffee.Web database update && cd ..

run:
	dotnet run -p SolarCoffee.Web

watchrun:
	dotnet watch run -p SolarCoffee.Web
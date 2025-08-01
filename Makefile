#Makefile
.PHONY: up clean dev prod

ENV_FILE ?= .env.development

up: $(ENV_FILE)
	@docker compose --env-file $(ENV_FILE) up

dev: .env.development
	@$(MAKE) up ENV_FILE=.env.development

prod: .env.production
	@$(MAKE) up ENV_FILE=.env.production

.env.development: .secrets/service-account.json
	@echo "Generating development environment..."
	@test -f "$<" || { echo "Error: $< not found"; exit 1; }
	@echo "FIREBASE_CREDS_BASE64=$$(base64 -w 0 "$<")" > $@
	@echo "ASPNETCORE_ENVIRONMENT=Development" >> $@
	@chmod 600 $@

.env.production: .secrets/service-account.production.json
	@echo "Generating production environment..."
	@test -f "$<" || { echo "Error: $< not found"; exit 1; }
	@echo "FIREBASE_CREDS_BASE64=$$(base64 -w 0 "$<")" > $@
	@echo "ASPNETCORE_ENVIRONMENT=Production" >> $@
	@chmod 600 $@

clean:
	rm -f .env.development .env.production .env

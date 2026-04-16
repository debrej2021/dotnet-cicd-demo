# GitHub Secrets Setup Guide

Before the pipeline runs, add these secrets to your GitHub repository.

## How to add secrets:
Go to: GitHub Repo → Settings → Secrets and variables → Actions → New repository secret

---

## Required Secrets

| Secret Name         | Value                          | Purpose                         |
|---------------------|--------------------------------|---------------------------------|
| DOCKER_USERNAME     | Your Docker Hub username       | Login to Docker Hub             |
| DOCKER_PASSWORD     | Your Docker Hub access token   | Login to Docker Hub             |

## Optional Secrets (if deploying to AWS)

| Secret Name              | Value                    | Purpose                    |
|--------------------------|--------------------------|----------------------------|
| AWS_ACCESS_KEY_ID        | Your AWS access key      | AWS authentication         |
| AWS_SECRET_ACCESS_KEY    | Your AWS secret key      | AWS authentication         |

## Optional Secrets (if deploying to Azure)

| Secret Name         | Value                          | Purpose                         |
|---------------------|--------------------------------|---------------------------------|
| AZURE_CREDENTIALS   | JSON from `az ad sp create`    | Azure authentication            |

---

## How to get a Docker Hub Access Token (safer than password):
1. Login to hub.docker.com
2. Account Settings → Security → New Access Token
3. Copy the token and use it as DOCKER_PASSWORD

---

## GitHub Environments (for manual approval on prod):
1. Go to: Repo → Settings → Environments
2. Create environment named: `staging`
3. Create environment named: `production`
4. On `production` → Add required reviewers (yourself or team)

This ensures production deploy waits for your manual approval click.

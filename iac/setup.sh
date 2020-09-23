#!/bin/bash

RG_NAME="rg-basta-2020-aks"
AZ_REGION=westeurope
az group create -n $RG_NAME -l $AZ_REGION

az acr create -n basta2020aks -l $AZ_REGION -g $RG_NAME --sku Basic --admin-enabled false

ACR_ID=$(az acr show -n basta2020aks -g $RG_NAME --query 'id' -o tsv)

az aks create -n basta2020aks -l $AZ_REGION -g $RG_NAME --enable-managed-identity --node-count 2 --attach-acr $ACR_ID
# Invoice ADO CRUD

## Description

RESTApi project following DDD Architecture

## CRUD Methods

## Application Flow

POST /api/clients
    ↓
[ClientCreateDto] (Request)
    ↓
Controller recibe ClientCreateDto
    ↓
Service convierte a ClientEntity
    ↓
Repository guarda ClientEntity
    ↓
Service convierte a ClientResponseDto
    ↓
[ClientResponseDto] (Response)
    ↓
HTTP 201 Created con ClientResponseDto en el body
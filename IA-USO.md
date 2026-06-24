# Declaração de Uso de IA/LLM

**Nome:** Riciere Marcone  
**Data:** 23/06/2026  
**Modelo(s) de IA/LLM utilizado(s):** Claude Sonnet  
**Ferramentas auxiliares:** Claude.ai

## 1) Nível de uso por parte do desafio

- Parte A (SQL): ☐ Consultei IA ☑ Usei IA para gerar parte do código
- Parte B (C#): ☐ Consultei IA ☑ Usei IA para gerar parte do código

## 2) O que a IA produziu (3–6 linhas por parte)

### A)

A IA gerou uma primeira versão da stored procedure `sp_ConsolidarVendasMensais`, incluindo a estrutura de JOINs, o filtro pelo mês corrente, a verificação de duplicidade com `NOT EXISTS` e o tratamento de erro com `TRY/CATCH`. A partir disso, revisei a lógica linha a linha e ajustei o código para ficar alinhado às regras de negócio do enunciado.

### B)

A IA gerou uma primeira versão da refatoração do controller legado, com separação em `Controller`, `Service` e `Repository`, além da criação dos DTOs e do endpoint `GET /api/turmas/{id}`. Depois disso, eu revisei a estrutura, reorganizei arquivos, escrevi os testes automatizados com xUnit para o `TurmaService` e validei localmente o comportamento da API antes da entrega.

## 3) Prompts principais (cole abaixo)

- Pedido de geração da stored procedure conforme as regras de negócio do enunciado.
- Pedido de refatoração do controller legado e implementação do endpoint `GET /api/turmas/{id}`.
- Pedido de um esqueleto de testes com xUnit para eu completar por conta própria.
- Revisão de duas implementações de teste e ajuda para debugar erros de build no Visual Studio / dotnet CLI.

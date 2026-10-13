# Reserva de Salas — Stage 1

API de reserva de salas de reunião.

Projeto de treino. Escopo: 2 semanas.

---

## Stack

- Minimal API (.NET)
- EF Core com SQLite
- xUnit para testes

---

## Por que esse projeto

Ele obriga você a lidar com tempo, null, falha externa e regra de domínio.

---

## Regras de negócio

1. **Reserva** tem sala, início e fim. O fim vem depois do início.

2. **Sem sobreposição:** duas reservas não ocupam a mesma sala no mesmo horário.

3. **Sem passado:** ninguém reserva um horário que já passou.

4. **Expiração:** a reserva nasce "Pendente". Se ninguém confirmar em 15 minutos, ela expira.

5. **Cancelamento:** só pode até 1 hora antes do início.

6. **Feriado:** a API consulta a BrasilAPI (`/api/feriados/v1/{ano}`) e bloqueia reservas em feriado. A chamada pode falhar. Você decide o que fazer nesse caso.

7. **Busca por id:** a reserva pode não existir. O endpoint responde certo nesse caso.

---

## Plano

**Semana 1:** entidade, regras 1 a 5, testes de cada regra.

**Semana 2:** endpoints, BrasilAPI, tratamento de falha, `docs/erros.md` em dia.

---

## Definição de pronto

Uma regra só fica pronta quando tem teste passando.

Todo bug vira uma linha em `docs/erros.md`.

---

## Antes de revisar qualquer código

Responda:

1. O que entra e o que sai?
2. Qual linha roda primeiro?
3. O que acontece se isso vier null?

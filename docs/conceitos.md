# Conceitos

Anotações do que já foi discutido.

---

## Datas e fuso

**UTC é a referência única.** O banco guarda tudo em UTC. Assim o fuso do servidor deixa de importar.

**O problema é a entrada.** Se o cliente manda `"2026-10-20T14:00:00"` sem fuso, a API não sabe de onde é esse 14:00.

**Converter exige saber a origem.** São Paulo é UTC-3. 14:00 lá vira 17:00 em UTC.

**Erro silencioso:** se a API achar que 14:00 já é UTC, a reserva fica 3 horas errada. Nenhum erro aparece.

**Pesquise:** RFC 3339 e `DateTimeOffset` na doc da Microsoft.

---

## Pergunta em aberto

Reescreva o JSON abaixo de um jeito que a API saiba, sem dúvida, qual instante o usuário quis dizer.

```json
{ "inicio": "2026-10-20T14:00:00" }
```

Sua resposta:

DateTimeOffset armazena o valor DateTime junto com a propriedade Offset
Offset é a diferença entre o horário local e o UTC

> [!NOTE]
> DateTime não tem campo para offset. No parse, ele converte para o fuso do servidor e descarta o -03:00.

 

```
{ "inicio": "2026-10-20T14:00:00-03:00" }
```
Por que **DateTime**, ao receber o Json acima, perderia o -03:00?
DateTime só registra a data de hora do dia, portanto, uma propriedade não suportaria o valor -03:00 na conversão 


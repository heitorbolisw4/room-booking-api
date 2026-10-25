# Erros

Uma linha por bug: o que aconteceu, por quê, como corrigiu.

| Data | O que aconteceu | Por quê | Como corrigiu |
|------|-----------------|---------|---------------|

25/10 -> Erro com ExecuteUpdateAsync no SQLite: o banco não consegue converter a query com enum diretamente. Apliquei um comando bruto em SQL com ExecuteSqlInterpolatedAsync

var pendente = (int)ReservaStatus.Pendente;
var cancelada = (int)ReservaStatus.Cancelada;

await context.Database.ExecuteSqlInterpolatedAsync(
    $"UPDATE Reservas SET Status = {cancelada} WHERE Status = {pendente} AND ExpiraEm <= '{agora:O}'",
    stoppingToken);   
# КТ №22 — События: комплексное задание

**Вариант 2 — Система достижений**

- Класс `Player` с событиями `event EventHandler<(int OldScore, int NewScore)>? ScoreChanged` и `event EventHandler<string>? AchievementUnlocked` и методом `AddScore(int amount)`, обновляющим очки и вызывающим `ScoreChanged`.
- Класс `AchievementTracker` в конструкторе подписывается на `Player.ScoreChanged` и в своём обработчике проверяет, не превысили ли очки порог — если да, инициирует `Player.AchievementUnlocked` с названием достижения.
- На `Player.AchievementUnlocked` подписан отдельный обработчик, который выводит сообщение о разблокированном достижении.
- Цепочка событий: `ScoreChanged` → обработчик `AchievementTracker` → `AchievementUnlocked`.
- Отписка `AchievementTracker` от `Player.ScoreChanged`: после неё достижения больше не разблокируются.

Дополнительно: `Player` реализует интерфейс `IScoreSource`, трекер работает через него; ввод чисел — метод с делегатом `Action<int>`, ошибки ввода обрабатываются через `try`/`catch`; при разблокировке достижения выводится «Вы победили! Игра закончена.» и программа завершается.

## Проверочные ключи

| Действие | Ожидаемое поведение |
|---|---|
| `AddScore(60)` | `ScoreChanged` срабатывает, очков 60, порог не превышен, достижение не разблокировано |
| `AddScore(50)` | очков 110, порог превышен, `AchievementUnlocked` срабатывает через `AchievementTracker` |
| отписка `AchievementTracker` от `ScoreChanged` | подтверждена (например, логом) |
| `AddScore(200)` после отписки | `ScoreChanged` срабатывает, но `AchievementUnlocked` больше не вызывается |

## Запуск

```
dotnet run
```

## Результат выполнения

![Результат выполнения](screenshot.png)

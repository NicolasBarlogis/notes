[[Arrange Act Assert]] kata
https://github.com/katalogs/learning-hours/tree/main/clean-testing/1-test-anatomy

## Typescript
### Démonstration
Le test à implémenter (le premier cas):
```typescript
import {Submarine} from "../src/Submarine";

test('1-New Submarine should have a correct starting position', () => {
	// Given - Arrange
	let submarine: Submarine = new Submarine();

	// When - Act

	
	// Then - Assert
	expect(submarine.depth).toBe(0);
	expect(submarine.position).toBe(0);
})
```

#### snippets
```typescript
test('Basic submarine', () => {
	global.performance = performance; 

	let sub: Submarine = new Submarine();
	sub = sub.execute(CommandBuilder.Down(5));
	expect(sub.aim).toBe(5);
})
```

```typescript
import {Submarine} from "../src/Submarine";
import {CommandBuilder} from "../src/CommandBuilder";
import {Command} from "../src/Command";
import { readFileSync } from 'fs';

test('Full scenario', () => {
	let sub: Submarine = new Submarine();
	let commands: Command[] = getCommandsFromCommandsFile();

	commands.forEach(command => sub = sub.execute(command));

	expect(sub.depth).toBe(987457);
	expect(sub.position).toBe(2162);
	expect(sub.aim).toBe(1051);
})
```
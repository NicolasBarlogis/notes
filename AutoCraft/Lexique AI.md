ReAct (ou ReAct agent)
:  AI agent (les plus communs), qui suivent le ReAct Framework -> Reason and Act.

MCP Model Context Protocl
: Standard pour connecter des IA à des sources de données

**Modèle apprenant**
Sans passer par un entraiment from scratch (trop de data / puissance), sur les LLM actuels, peux se faire via:
* Fine-tuning: fournir input/ouput d'exemples sur une tâche précise --> rend le modèle meilleur à cette tâche, et généralement moins bon sur le reste. Besoin de centaines/milliers d'exemples propres. Généralement one shot (pas d'apprentissage continu). Reste long et gourmand en ressources. [Unsloth](https://github.com/unslothai/unsloth) outil opensource permettant de le faire. [Tuto](https://www.youtube.com/watch?v=pTaSDVz0gok)
* [Few shot prompting](https://www.promptingguide.ai/techniques/fewshot) / example prompting: fournir quelques exemples de la tâche demandée pour aider le LLM à faire sa réponse
*  [Chain of though prompting](https://www.promptingguide.ai/techniques/cot): même idée que few shot, mais en détaillant la réflexion
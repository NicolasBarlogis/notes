That’s where the trilemma comes into play. You can’t have all 3 of the following attributes:

-   **_Domain model completeness_** — When all the application’s domain logic is located in the domain layer, i.e. not fragmented.
    
-   **_Domain model purity_** — When the domain layer doesn’t have out-of-process dependencies.
    
-   **_Performance_**, which is defined by the presence of unnecessary calls to out-of-process dependencies.
    

You have 3 options here, but each of them only gives you 2 out of the 3 attributes:

-   **_Push all external reads and writes to the edges of a business operation_** — Preserves domain model completeness and purity but concedes performance.
    
-   **_Inject out-of-process dependencies into the domain model_** — Keeps performance and domain model completeness, but at the expense of domain model purity.
    
-   **_Split the decision-making process between the domain layer and controllers_** — Helps with both performance and domain model purity but concedes completeness. With this approach, you need to introduce decision-making points (business logic) in the controller.

https://enterprisecraftsmanship.com/posts/domain-model-purity-completeness/
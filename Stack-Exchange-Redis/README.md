# Semanticas de entrega

En la mensajería, la semántica de entrega se refiere a la garantía de que un mensaje se entregará correctamente a su destino. Dependiendo de la aplicación y del sistema de mensajería utilizado, existen diferentes niveles de garantía de entrega que se pueden ofrecer. Algunas de las semánticas de entrega comunes incluyen:

- [**Como mucho una vez (at-most-once)**](./01-at-most-once-semantic/README.md): El mensaje se entrega una vez o no se entrega en absoluto.

- **Al menos una vez (at-least-once)**: El mensaje se entrega al menos una vez, pero puede haber duplicados.

- **Exactamente una vez (exactly-once)**: El mensaje se entrega exactamente una vez, sin duplicados ni pérdidas.

# Transacciones en Redis

Las transacciones en Redis permiten agrupar un conjunto de comandos en una única operación atómica. Esto significa que todos los comandos en la transacción se ejecutarán como una sola unidad, garantizando que no se interrumpan entre sí. Si alguno de los comandos falla, se deshacen todos los cambios realizados hasta ese momento.

Las transacciones en Redis se pueden iniciar con el comando `MULTI`, seguido de una serie de comandos, y se pueden ejecutar con el comando `EXEC`. También se pueden descartar con el comando `DISCARD`.

```mermaid
graph TD
    A[Proceso de Envío] -->|Envía mensaje| B[Cola de Mensajes]
    B[Cola de Mensajes] -->|Recibe mensaje| C[Proceso de Recepción]
    A -->|Envía mensaje| B
    B -->|Recibe mensaje| C

    subgraph "Cola de Mensajes"
        direction TB
        D1[Mensaje 1]
        D2[Mensaje 2]
        D3[Mensaje 3]
    end

    subgraph "Gestión de Transacciones"
        E1[Inicio de Transacción]
        E2[Envía Mensaje]
        E3[Confirma Transacción]
        E4[Recibe Mensaje]
        E5[Procesa Mensaje]
        E6[Finaliza Transacción]
    end

    E1 --> E2
    E2 --> B
    B --> E3
    E3 --> E4
    E4 --> E5
    E5 --> E6

    style A fill:#09f,stroke:#333,stroke-width:2px,color:black;
    style B fill:#0f9,stroke:#333,stroke-width:2px,color:black;
    style C fill:#0f9,stroke:#333,stroke-width:2px,color:black;
    style D1 fill:#0cc,stroke:#333,stroke-width:2px,color:black;
    style D2 fill:#0cc,stroke:#333,stroke-width:2px,color:black;
    style D3 fill:#0cc,stroke:#333,stroke-width:2px,color:black;
    style E1 fill:#0cf,stroke:#333,stroke-width:2px,color:black;
    style E2 fill:#0cf,stroke:#333,stroke-width:2px,color:black;
    style E3 fill:#0cf,stroke:#333,stroke-width:2px,color:black;
    style E4 fill:#0cf,stroke:#333,stroke-width:2px,color:black;
    style E5 fill:#0cf,stroke:#333,stroke-width:2px,color:black;
    style E6 fill:#0cf,stroke:#333,stroke-width:2px,color:black;
```

```mermaid
sequenceDiagram
    participant TM as Gestor de Transacciones
    participant A1 as Proceso A1
    participant A2 as Proceso A2
    participant An as Proceso An

    TM->>A1: Iniciar Proceso
    A1->>TM: Resultado del Proceso
    alt Proceso A1 exitoso
        TM->>A1: Commit
    else Proceso A1 falla
        TM->>A1: Rollback (Conciliation)
    end

    TM->>A2: Iniciar Proceso
    A2->>TM: Resultado del Proceso
    alt Proceso A2 exitoso
        TM->>A2: Commit
    else Proceso A2 falla
        TM->>A2: Rollback (Conciliation)
        TM->>A1: Rollback (Conciliation)
    end

    TM->>An: Iniciar Proceso
    An->>TM: Resultado del Proceso
    alt Proceso An exitoso
        TM->>An: Commit
    else Proceso An falla
        TM->>An: Rollback (Conciliation)        
        TM->>A2: Rollback (Conciliation)
        TM->>A1: Rollback (Conciliation)
    end
```


# Worker Services en ASP .NET 

Los Worker Services en ASP .NET son una forma de crear aplicaciones de fondo en .NET Core. Estas aplicaciones se ejecutan en segundo plano y pueden realizar tareas como procesamiento de colas, procesamiento de mensajes, tareas de mantenimiento, etc. Los Worker Services son una forma eficiente de ejecutar tareas en segundo plano en .NET Core y se pueden implementar como servicios de Windows, servicios de Linux o contenedores de Docker.

# Referencias








[<RequireQualifiedAccess>]
module Components.Counter

open Lit
open Haunted

let private counter (props: {| initial: int option |}) =
  let count, setCount =
    Haunted.useState (defaultArg props.initial 0)

  html
    $"""
        <p>Home: {count}</p>
        <button @click={fun _ -> setCount (count + 1)}>Increment</button>
        <button @click={fun _ -> setCount (count - 1)}>Decrement</button>
        <button @click={fun _ -> setCount (defaultArg props.initial 0)}>Reset</button>
        """

let register () =
  defineComponent "flit-counter" (Haunted.Component counter)

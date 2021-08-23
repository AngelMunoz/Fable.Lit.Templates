[<RequireQualifiedAccess>]
module Pages.Home

open Lit
open Fable.Haunted


let private counter (props: {| initial: int option |}) =
    let count, setCount =
        Haunted.useState (props.initial |> Option.defaultValue 0)

    html
        $"""
        <p>Home: {count}</p>
        <button @click={fun _ -> setCount (count + 1)}>Increment</button>
        <button @click={fun _ -> setCount (count - 1)}>Decrement</button>
        <button @click={fun _ -> setCount (0)}>Reset</button>
        """

let register () =
    defineComponent "flit-home" (Haunted.Component counter)

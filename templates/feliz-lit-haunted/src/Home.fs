[<RequireQualifiedAccess>]
module Pages.Home

open Lit.Feliz
open Haunted


let private counter (props: {| initial: int option |}) =
  let count, setCount =
    Haunted.useState (props.initial |> Option.defaultValue 0)

  Html.div [
    Html.p $"Home: {count}"
    Html.button [
      Ev.onClick (fun _ -> setCount (count + 1))
      Html.text "Increment"
    ]
    Html.button [
      Ev.onClick (fun _ -> setCount (count - 1))
      Html.text "Decrement"
    ]
    Html.button [
      Ev.onClick (fun _ -> setCount (0))
      Html.text "Reset"
    ]
  ]
  |> Feliz.toLit

let register () =
  defineComponent
    "flit-home"
    (Haunted.Component(counter, {| useShadowDOM = false |}))

module Components.Navbar

open Lit
open Haunted
open Types
open Fable.Core.JsInterop
open Browser.Types


let private navabar () =
  let that: HTMLElement = jsThis

  let goBack _ =
    let evt =
      Haunted.createEvent ("go-back", {| composed = true; bubbles = true |})

    that.dispatchEvent evt

  let goTo page _ =
    let evt =
      Haunted.createCustomEvent (
        "go-to-page",
        {| composed = true
           bubbles = true
           detail = page |}
      )

    that.dispatchEvent evt

  html
    $"""
      <nav>
        <button @click={goBack}>Go Back</button>
        <button @click={goTo Page.Home}>Home</button>
        <button @click={goTo Page.Notes}>Notes</button>
      </nav>
      """

let register () =
  defineComponent "flit-navbar" (Haunted.Component navabar)

[<RequireQualifiedAccess>]
module Pages.Home

open Lit
open Haunted


let private home (props: {| initial: int option |}) =
  html
    $"""
        <article>
          <section>
            <h1>Counter at 0</h1>
            <flit-counter></flit-counter>
          </section>
          <section>
            <h1>Counter at 100</h1>
            <flit-counter .initial={100}></flit-counter>
          </section>
        </article>
        """

let register () =
  defineComponent "flit-home" (Haunted.Component home)

[<RequireQualifiedAccess>]
module Pages.Notes

open Browser.Types
open Lit
open Fable.Haunted



type private Note =
    { Id: int
      Title: string
      Body: string }

type private State =
    { CurrentNote: Note option
      Notes: Note list }

type private Msg =
    | Save
    | Remove of Note
    | SetTitle of string
    | SetBody of string

let private update state msg =
    match msg with
    | Save ->
        match state.CurrentNote
              |> Option.map
                  (fun note ->
                      { note with
                            Id = (state.Notes |> Seq.length) + 1 }) with
        | Some note ->
            { state with
                  Notes =
                      { note with
                            Id = state.Notes.Length + 1 }
                      :: state.Notes }
        | None -> state

    | Remove note ->
        { state with
              Notes = state.Notes |> List.filter (fun n -> n <> note) }
    | SetTitle title ->
        let current =
            state.CurrentNote
            |> Option.map (fun current -> { current with Title = title })
            |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

        { state with CurrentNote = current }
    | SetBody body ->
        let current =
            state.CurrentNote
            |> Option.map (fun current -> { current with Body = body })
            |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

        { state with CurrentNote = current }




let private noteTemplate note =
    html
        $"""
        <li>{note.Id} - {note.Title}</li>
    """

let private view () =

    let (state, dispatch) =
        Haunted.useReducer (update, { CurrentNote = None; Notes = [] })

    let notes = state.Notes |> List.map noteTemplate

    html
        $"""
        <form @submit="{fun (ev: Event) ->
                            ev.preventDefault ()
                            dispatch Save}">
            <input
                type="text"
                name="title"
                placeholder="Title"
                @keyup="{fun (evt: KeyboardEvent) ->
                             SetTitle (evt.target :?> HTMLInputElement).value
                             |> dispatch}" />
            <input
                type="text"
                name="body"
                placeholder="Body"
                @keyup="{fun (evt: KeyboardEvent) ->
                             SetBody (evt.target :?> HTMLInputElement).value
                             |> dispatch}" />
            <button type="submit">Add</button>
        </form>
        <ul>{notes}</ul>
        """

let register () =
    defineComponent "flit-notes" (Haunted.Component view)

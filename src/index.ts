import { SentryWorker } from "@okkema/worker/sentry"
import { API, AuthBindings, authorize, AuthVariables } from "@okkema/worker/api"

type Environment = {
  SENTRY_DSN: string
  TMDB_TOKEN: string
  DEBUG: string
  REDIRECT: string
} & AuthBindings

type Variables = AuthVariables

const BASE_URL_V3 = "https://api.themoviedb.org/3"

export default SentryWorker<Environment>({
  fetch(req, env, ctx) {
    const app = API<Environment, Variables>({
      tokenUrl: `https://${env.OAUTH_TENANT}/oauth/token`,
      scopes: {
        "read:movies": "Read TMDB movies"
      },
      options: {
        docs_url: null
      }
    })
    app.get("/movie/*", authorize("read:movies"), async function (c) {
      c.res = await fetch(`${BASE_URL_V3}${new URL(c.req.url).pathname}`, {
        headers: {
          "Authorization": `Bearer ${env.TMDB_TOKEN}`
        }
      })
    })
    if (new URL(req.url).pathname === "/") 
      return new Response("Redirecting...", { 
        status: 301, 
        headers: {
          Location: env.REDIRECT,
        }
      })
    return app.fetch(req as any, env, ctx) as any
  }
})

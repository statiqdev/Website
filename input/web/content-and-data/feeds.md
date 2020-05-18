Order: 8
---
Feed files let you define RSS, Atom, and/or RDF feeds for any content or data in your site.

Feed files can be named anything you like and should be one of the supported data formats such as `.json`, `.yaml`, or `.yml`. They are identified by having metadata values for `FeedRss`, `FeedAtom`, and/or `FeedRdf`.

The following metadata controls the feed:

- `FeedPipelines`: The pipeline(s) to get documents for the feed from. Defaults to the `Content` pipeline if not defined.
- `FeedSources`: A globbing pattern to filter documents from the feed pipeline(s) based on source path (or all documents from the pipeline(s) if not defined).
- `FeedFilter`: An additional metadata filter for documents from the feed pipeline(s) that should return a `bool`.
- `FeedOrderKey`: The metadata key that sorting should be based on.
- `FeedOrderDescending`: Indicates that feed items should be sorted in descending order.
- `FeedSize`: The number of items the feed should contain after sorting.
- `FeedRss`: A `bool` indicating if an RSS feed should be output.
- `FeedAtom`: A `bool` indicating if an Atom feed should be output.
- `FeedRdf`: A `bool` indicating if an RDF feed should be output.

The following metadata describes the feed:

- `FeedId`: A URI that links to the root of the site by default.
- `FeedTitle`: The title of the feed or site (defaults to the global `Title` setting).
- `FeedDescription`: A description of the feed or site (defaults to the global `Description` setting).
- `FeedAuthor`: The author of the feed (generally an email address).
- `FeedPublished`: The date the feed was published (defaults to the current date).
- `FeedUpdated`: The date the feed was updated (defaults to the current date).
- `FeedLink`: A URI to the feed location (defaults to the destination path).
- `FeedImageLink`: An image to associate with the feed.

The following metadata describes items in the feed and should be set in the feed item documents to override the defaults (all are optional and the feed will use sensible defaults if these aren't defined):

- `FeedItemId`: A unique ID (usually a link) to the feed item (defaults to an absolute link to the document).
- `FeedItemTitle`: The title of the feed item (defaults to the value of `Title`).
- `FeedItemDescription`: A description of the feed item (defaults to the value of `Description` and falls back to the value of `Excerpt` if `Description` is not defined).
- `FeedItemAuthor`: The author of the feed item (usually an email address).
- `FeedItemPublished`: The date the item was published (defaults to the value of `Published`).
- `FeedItemUpdated`: The date the item was updated (defaults to the value of `Published`).
- `FeedItemLink`: A link to the feed item (defaults to an absolute link to the document).
- `FeedItemImageLink`: An absolute link to an image that represents the feed item (defaults to the value of `Image`).
- `FeedItemContent`: The full content of the feed item (defaults to the value of `Content` or the full document content).
- `FeedItemThreadLink`: A link to a comments thread for the feed item.
- `FeedItemThreadCount`: The number of comments on the feed item.
- `FeedItemThreadUpdated`: The date the comments thread and count were updated.
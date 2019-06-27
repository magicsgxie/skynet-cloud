using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class TemplateFunctionView : IFunctionView
    {
        private const int InvalidArgumentIndex = -1;
        private static readonly Regex SplitRegex = new Regex("(\\?[0-9]+)");

        private struct TemplateChunk
        {
            public string Text; // including prefix if parameter
            public int ArgumentIndex;

            public TemplateChunk(string chunk, int argIndex)
            {
                Text = chunk;
                ArgumentIndex = argIndex;
            }
        }

        private int allowedArgsCount;

        private readonly string template;
        private TemplateChunk[] chunks;


        public TemplateFunctionView(string template)
        {
            this.template = template;

            InitFromTemplate();
        }

        private void InitFromTemplate()
        {
            string[] stringChunks = SplitRegex.Split(template);
            chunks = new TemplateChunk[stringChunks.Length];
            var set = new HashSet<int>();
            for (int i = 0; i < stringChunks.Length; i++)
            {
                string chunk = stringChunks[i];
                if (i % 2 == 0)
                {
                    // Text part.
                    chunks[i] = new TemplateChunk(chunk, InvalidArgumentIndex);
                }
                else
                {
                    // Separator, i.e. argument
                    int argIndex = int.Parse(chunk.Substring(1), CultureInfo.InvariantCulture);
                    chunks[i] = new TemplateChunk(stringChunks[i], argIndex);
                    set.Add(argIndex);
                }
            }
            allowedArgsCount = set.Count;
        }

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (allowedArgsCount != args.Length)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, template, "template", allowedArgsCount));

            foreach (TemplateChunk tc in chunks)
            {
                if (tc.ArgumentIndex != InvalidArgumentIndex)
                {
                    int adjustedIndex = tc.ArgumentIndex - 1; // Arg indices are one-based
                    var arg = adjustedIndex < args.Length ? args[adjustedIndex] : null;
                    if (arg != null)
                        builder.Visit(arg);
                }
                else
                    builder.Append(tc.Text);
            }

        }

        public override string ToString()
        {
            return template;
        }
    }
}

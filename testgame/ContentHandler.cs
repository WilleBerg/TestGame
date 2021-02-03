using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace testgame {
    public class ContentHandler : ContentManager {
        public ContentHandler(IServiceProvider serviceProvider, string rootDirectory) : base(serviceProvider, rootDirectory) {
        }
    }
}

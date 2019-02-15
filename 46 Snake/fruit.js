function Fruit(boundaries, scale) { 
  this.scale = scale;
  this.boundaries = boundaries.map(x => x - scale);
  this.pos = [getRandomArbitrary(0, boundaries[0], scale),
              getRandomArbitrary(0, boundaries[1], scale)];
  
  this.show = function() {
    fill(252, 151, 42);
    rect(this.pos[0], this.pos[1], this.scale, this.scale);
  }
}
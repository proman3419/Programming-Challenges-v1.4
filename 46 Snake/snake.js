function Snake(boundaries, scale) {
  this.scale = scale;
  this.boundaries = boundaries.map(x => x - this.scale);
  this.pos = [getRandomArbitrary(0, boundaries[0], scale),
              getRandomArbitrary(0, boundaries[1], scale)];
  this.dir = [0, 0];
  this.history = [[0, 0]];
  
  this.update = function() {
    this.history.shift()
    
    var new_x = this.pos[0] + this.dir[0];
    var new_y = this.pos[1] + this.dir[1];
    
    if ((0 > new_x || new_x >= this.boundaries[0]) ||
        (0 > new_y || new_y >= this.boundaries[1]) ||
        this.selfCrush()) {
          return true;
        }
    
    this.pos[0] = new_x;
    this.pos[1] = new_y;
    this.history.push([new_x, new_y]);  
  }
  
  this.show = function() {
    fill(255)
    for (var i = 0; i < this.history.length; i++) {
      rect(this.history[i][0], this.history[i][1], 
           this.scale, this.scale);
    }
  }
  
  this.eat = function() {
    this.history.unshift(this.pos);
  }
  
  this.selfCrush = function() {
    return this.history.filter(x => x.toString() == 
                               this.pos.toString()).length >= 2;
  }
}
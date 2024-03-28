public interface IDamageable
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }

    void takeDamage(int damaage);
}